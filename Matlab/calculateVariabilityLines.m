
%close all;
% A1 = repmat(1:10, 10,1);
% A1 = A1 .*  repmat((0.1:0.1:1)', 1, 10) .* rand(10, 10);
% A2 = (repmat(10, 10, 10) - A1) * 2;
% B = repmat(1:10, 10,1);
% A1 = [A1, B];
% A2 = [A2, B];
% A = [A1; A2];

%streamlines = A;
%load('streamlines.mat');
streamlines = connections';
% 
% highNumberSamples = 1;

pYMin = 1;
pYMax = (size(streamlines, 1) / 2);
pXMin  = pYMax + 1;
pXMax = size(streamlines, 1);

%figure;
%subplot(3, 2, 1);
if ~highNumberSamples
	%plot(streamlines(pXMin:pXMax, :), streamlines(pYMin:pYMax, :));
end
colors = 'rbgycm';

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

sampleOffset = 2;
numSamples = 1000;

meanVector = determineMeanVector(streamlines);

streamlines = determineMeanSubtracted(streamlines, meanVector);

%subplot(3, 2, 2);
if ~highNumberSamples
	%plot(streamlines(pXMin:pXMax, :), streamlines(pYMin:pYMax, :));
end

if ~highNumberSamples
	[eigenvectors, eigenvalues] = eig(streamlines' * streamlines, 'vector');
else
    [eigenvectors, eigenvalues] = eig(streamlines * streamlines', 'vector');
end
[eigenvectors, eigenvalues] = eigsort(eigenvectors, eigenvalues);


if ~highNumberSamples
	basis = determineBasis(streamlines, eigenvectors);
else
    basis = eigenvectors;
end
reducedStreamlines = reduceData(basis, numBasis, streamlines, meanVector);

lineIDs = kmeans(reducedStreamlines', numClusters);
% lineIDs = clusterdata(reducedStreamlines', 'maxclust', numClusters, 'distance', 'euclidean', 'linkage', 'average')';
% dendrogram(linkage(reducedStreamlines', 'average'),'Orientation','left','ColorThreshold',14);

centerLines = zeros(numClusters, numBasis);

%subplot(3, 2, 3);
%hold on;
for i = 1:numClusters
    centerLines(i, :) = median(reducedStreamlines(:, lineIDs==i), 2)';
    
    if (numBasis > 1) && ~highNumberSamples
        %plot(reducedStreamlines(1, lineIDs==i),reducedStreamlines(2, lineIDs==i), strcat('.', colors(mod(i, 6))), 'MarkerSize',12);
    end
    
end
%plot(centerLines(:,1),centerLines(:,2),'kx', 'MarkerSize',15,'LineWidth',3);
%hold off;

reconStreamlines = reconstructData(basis, numBasis, reducedStreamlines, meanVector);
reconCenterLines = reconstructData(basis, numBasis, centerLines', meanVector);

%
%subplot(3, 2, 4);
%hold on;
if ~highNumberSamples
    for i = 1:numClusters
       %plot(reconStreamlines(pXMin:pXMax, (lineIDs == i)), reconStreamlines(pYMin:pYMax, (lineIDs == i)),  colors(mod(i, 6)), 'LineWidth', 2);
    end
end
%plot(reconCenterLines(pXMin:pXMax, :), reconCenterLines(pYMin:pYMax, :), 'black', 'linewidth', 2);
%hold off;

threshold = sqrt(chi2inv(convInter,2));

%subplot(3, 2, 5);
%hold on;

for i = 1:numClusters
    gridStreamlines = reducedStreamlines(:,lineIDs == i);
    minStreamlines = min(gridStreamlines, [], 2) - repmat(sampleOffset, numBasis, 1);
    maxStreamlines = max(gridStreamlines, [], 2) + repmat(sampleOffset, numBasis, 1);
%     gridOut = zeros(numSamples, numBasis);
%     gridCommandStart = '[';
%     gridCommandEnd = 'ndgrid(';
%     reshapeCommand = 'samples = [';
%     for d = 1:numBasis
%         gridOut(:, d) = linspace(min(gridStreamlines(:, d)) - sampleOffset, max(gridStreamlines(:, d)) + sampleOffset, numSamples);
%         gridCommandStart = strcat(gridCommandStart, 'samples', int2str(d),',');
%         gridCommandEnd = strcat(gridCommandEnd, 'gridOut(:,', int2str(d),'),');
%         reshapeCommand = strcat(reshapeCommand, 'samples', int2str(d), '(:),');
%     end
%     gridCommand = strcat(gridCommandStart(:, 1:(end - 1)), ']=', gridCommandEnd(:, 1:(end - 1)), ');');
%     reshapeCommand = strcat(reshapeCommand(:, 1:(end - 1)), '];');
%     eval(gridCommand);
%     eval(reshapeCommand);
    samples = rand(numBasis, numSamples) .* repmat((maxStreamlines - minStreamlines), 1, numSamples) + repmat(minStreamlines, 1, numSamples);
    samples = samples';
    if size(gridStreamlines, 2) > size(gridStreamlines, 1)
        mahalDist = mahal(samples, gridStreamlines');

        samplesInside = samples(mahalDist <= threshold, :)';
        eval(strcat('sampleStreamlines', int2str(i), '=reconstructData(basis, numBasis, samplesInside, meanVector);'));
        if ~highNumberSamples
            samplesInside = samplesInside';
        end
        %scatter(samplesInside(:, 1), samplesInside(:, 2), strcat(colors(mod(i, 6)), '*'));
    else
        eval(strcat('sampleStreamlines', int2str(i), '=reconCenterLines(:,',int2str(i),');'));
    end
end
%hold off;

%subplot(3, 2, 6);
% figure;
% hold on;
% load coastlines;
% plot(coastlon, coastlat);
percentCluster = zeros(numClusters, 1);
countClusterTotal = size(lineIDs, 1);
for i = 1:numClusters
%    eval(strcat('plot(sampleStreamlines', int2str(i), '(pYMin:pYMax, :), sampleStreamlines', int2str(i), '(pXMin:pXMax, :), ''', colors(mod(i-1, 6)+1), ''', ''linewidth'', 3);'));

    eval(strcat('sampleStreamlines', int2str(i), '=', 'sampleStreamlines', int2str(i), '''', ';'));
    eval(strcat('percentCluster(', int2str(i), ')=sum(lineIDs == ', int2str(i), ') / countClusterTotal;'));
end

% plot(reconCenterLines(pYMin:pYMax, :), reconCenterLines(pXMin:pXMax, :), 'black', 'linewidth', 2);
reconCenterLines = reconCenterLines';
% hold off;
% xlim([-40 60])
% ylim([30 80])

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
% 
% plot(boundary1(:, 1), boundary1(:, 2))
% hold on
% plot(boundary2(:, 1), boundary2(:, 2))
% plot(boundary3(:, 1), boundary3(:, 2))
% hold off