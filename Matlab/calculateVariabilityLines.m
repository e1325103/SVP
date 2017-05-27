%% Streamline Variability Calculation
% Transforms a given streamline with PCA into a another space. This
% streamline points in this space are sampled and the statistical
% parameters of each cluster (median, variance) are transformed back into
% the original space.

%% Parameter
%
% * *connections* m*n matrix with m data points (x-, y-coordinates) and n streamlines
% * *numClusters* Number of generated clusters _Default: 3_
% * *numBasis* Number of basis used for the reconstruction _Default: 3_
% * *convInter* Convidence intervall of the variability lines _Default: 0.9_
% * *highNumberSamples* 0 =  if more data points than streamlines; 1 = if
% more streamlines than samples (use transpose trick) _Default: 0_
% * *numSamples* Number of samples per cluster _Default: 30000_

pYMin = 1;
pYMax = (size(streamlines, 1) / 2);
pXMin  = pYMax + 1;
pXMax = size(streamlines, 1);
colors = 'rbgycm';

streamlines = connections';

if(exist('numClusters', 'var')~=1)
    numClusters = 3;
end

if(exist('highNumberSamples', 'var')~=1)
    highNumberSamples = 0;
end

if(exist('numBasis', 'var')~=1)
    numBasis = 3;
end

if(exist('convInter', 'var')~=1)
    convInter = 0.9;
end

if(exist('numSamples', 'var')~=1)
    numSamples = 30000;
end

sampleOffset = 2;

meanVector = mean(streamlines, 2);

streamlines = streamlines - repmat(meanVector, 1, size(streamlines, 2));

%% Calculate PCA Basis

if ~highNumberSamples
	[eigenvectors, eigenvalues] = eig(streamlines' * streamlines, 'vector');
else
    [eigenvectors, eigenvalues] = eig(streamlines * streamlines', 'vector');
end
[eigenvectors, eigenvalues] = eigsort(eigenvectors, eigenvalues);

%% Reduce Streamlines

if ~highNumberSamples
	basis = determineBasis(streamlines, eigenvectors);
else
    basis = eigenvectors;
end
reducedStreamlines = reduceData(basis, numBasis, streamlines);

%% Cluster Reduced Streamlines with k-Means

lineIDs = kmeans(reducedStreamlines', numClusters);

%% Calculate the Median Streamline for each Cluster

centerLines = zeros(numClusters, numBasis);

for i = 1:numClusters
    centerLines(i, :) = median(reducedStreamlines(:, lineIDs==i), 2)';
    
end

reconStreamlines = reconstructData(basis, numBasis, reducedStreamlines, meanVector);
reconCenterLines = reconstructData(basis, numBasis, centerLines', meanVector);

%% Uniformly Sample each Cluster's Convidence Lobe
% The region around all clusters is sampled using the Monte-Carlo Method.
% The points in a rectengular region are randomly chosen and only those who
% lie with in the bounds of the convidence elipsoid get chosen. To check if
% a point lies with the multidimensional elipsoid its Mahalanobis distance
% is calculated and compared against a threshold.

threshold = sqrt(chi2inv(convInter, numBasis));

for i = 1:numClusters
    gridStreamlines = reducedStreamlines(:,lineIDs == i);
    minStreamlines = min(gridStreamlines, [], 2) - repmat(sampleOffset, numBasis, 1);
    maxStreamlines = max(gridStreamlines, [], 2) + repmat(sampleOffset, numBasis, 1);
    samples = rand(numBasis, numSamples) .* repmat((maxStreamlines - minStreamlines), 1, numSamples) + repmat(minStreamlines, 1, numSamples);
    samples = samples';
    if size(gridStreamlines, 2) > size(gridStreamlines, 1)
        mahalDist = mahal(samples, gridStreamlines');

        samplesInside = samples(mahalDist <= threshold, :)';
        eval(strcat('sampleStreamlines', int2str(i), '=reconstructData(basis, numBasis, samplesInside, meanVector);'));
        if ~highNumberSamples
            samplesInside = samplesInside';
        end
    else
        eval(strcat('sampleStreamlines', int2str(i), '=reconCenterLines(:,',int2str(i),');'));
    end
end

percentCluster = zeros(numClusters, 1);
countClusterTotal = size(lineIDs, 1);
for i = 1:numClusters
    eval(strcat('sampleStreamlines', int2str(i), '=', 'sampleStreamlines', int2str(i), '''', ';'));
    eval(strcat('percentCluster(', int2str(i), ')=sum(lineIDs == ', int2str(i), ') / countClusterTotal;'));
end
reconCenterLines = reconCenterLines';

%% Calculate Boundary Region for the Sampled Lobes

% for i = 1:numClusters
%     I = zeros(1000, 1000, 'uint8');
%     eval(strcat('sampleStreamlines=sampleStreamlines', int2str(i), ';'));
%     for r = sampleStreamlines'
%         for c = 1:((size(r) / 2) - 1)
%             x = [r(c) r(c + 1)] * 2.5 + 250;
%             y = [r(pXMin + c - 1) r(pXMin + c)] * 2.5+250;
%             nPoints = max(abs(diff(x)), abs(diff(y)))+1;
%             rIndex = round(linspace(y(1), y(2), nPoints));
%             cIndex = round(linspace(x(1), x(2), nPoints));
%             index = sub2ind(size(I), rIndex, cIndex);
%             I(index) = 255; 
%         end
%     end
%     se = strel('square',2);
%     I = imdilate(I, se);
%     [x, y] = find(I);
%     k = boundary(x, y, 0.2);
%     x = x(k);
%     y = y(k);
%     eval(strcat('boundary', int2str(i), '=[x, y];'));
% end