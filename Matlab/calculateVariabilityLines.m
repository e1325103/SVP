% close all;
% A1 = repmat(1:10, 10,1);
% A1 = A1 .*  repmat((0.1:0.1:1)', 1, 10) .* rand(10, 10);
% A2 = (repmat(10, 10, 10) - A1) * 2;
% B = repmat(1:10, 10,1);
% A1 = [A1, B];
% A2 = [A2, B];
% A = [A1; A2];
%streamlines = A';

%
subplot(3, 2, 1);
plot(streamlines(11:20, :), streamlines(1:10, :));
%
colors = 'rbgycm';
if exists('convInter') == 0
    convInter = 0.95;
end
if exists('numBasis') == 0
    numBasis = 3;
end
if exists('numClusters') == 0
    numClusters = 2;
end
if exists('numSamples') == 0
    numSamples = 100;
end

meanVector = determineMeanVector(streamlines);

streamlines = determineMeanSubtracted(streamlines, meanVector);

%
subplot(3, 2, 2);
plot(streamlines(11:20, :), streamlines(1:10, :));
%

[eigenvectors, eigenvalues] = eig(streamlines' * streamlines, 'vector');
[eigenvectors, eigenvalues] = eigsort(eigenvectors, eigenvalues);

basis = determineBasis(streamlines, eigenvectors);
reducedStreamlines = reduceData(basis, numBasis, streamlines, meanVector);

lineIDs = kmeans(reducedStreamlines', numClusters);

centerLines = zeros(numClusters, numBasis);

subplot(3, 2, 3);
hold on;
for i = 1:numClusters
    centerLines(i, :) = median(reducedStreamlines(:, lineIDs==i), 2)';
    
    if (numBasis > 1)
        plot(reducedStreamlines(1, lineIDs==i),reducedStreamlines(2, lineIDs==i), strcat('.', colors(mod(i, 6))), 'MarkerSize',12);
    end
    
end
plot(centerLines(:,1),centerLines(:,2),'kx', 'MarkerSize',15,'LineWidth',3);
hold off;

reconStreamlines = reconstructData(basis, numBasis, reducedStreamlines, meanVector);
reconCenterLines = reconstructData(basis, numBasis, centerLines', meanVector);

%
subplot(3, 2, 4);
hold on;
for i = 1:numClusters
    plot(reconStreamlines(11:20, (lineIDs == i)), reconStreamlines(1:10, (lineIDs == i)),  colors(mod(i, 6)), 'LineWidth', 2^(i-1));
end
plot(reconCenterLines(11:20, :), reconCenterLines(1:10, :), 'black', 'linewidth', 2);
hold off;
%

threshold = sqrt(chi2inv(convInter,2));

subplot(3, 2, 5);
hold on;

for i = 1:numClusters
    gridStreamlines = reducedStreamlines(:,lineIDs == i);
    gridOut = zeros(numSamples, numBasis);
    gridCommandStart = '[';
    gridCommandEnd = 'ndgrid(';
    reshapeCommand = 'samples = [';
    for d = 1:numBasis
        gridOut(:, d) = linspace(min(gridStreamlines(:, d)) - 100, max(gridStreamlines(:, d)) + 100, numSamples);
        gridCommandStart = strcat(gridCommandStart, 'samples', int2str(d),',');
        gridCommandEnd = strcat(gridCommandEnd, 'gridOut(:,', int2str(d),'),');
        reshapeCommand = strcat(reshapeCommand, 'samples', int2str(d), '(:),');
    end
    gridCommand = strcat(gridCommandStart(:, 1:(end - 1)), ']=', gridCommandEnd(:, 1:(end - 1)), ');');
    reshapeCommand = strcat(reshapeCommand(:, 1:(end - 1)), '];');
    eval(gridCommand);
    eval(reshapeCommand);
    mahalDist = mahal(samples, gridStreamlines');
    
    samplesInside = samples(mahalDist <= threshold, :)';
    eval(strcat('sampleStreamlines', int2str(i), '=reconstructData(basis, numBasis, samplesInside, meanVector);'));
    scatter(samplesInside(:, 1), samplesInside(:, 2), strcat(colors(mod(i, 6)), '*'));
end
hold off;

subplot(3, 2, 6);
hold on;
for i = 1:numClusters
    eval(strcat('plot(sampleStreamlines', int2str(i), '(11:20, :), sampleStreamlines', int2str(i), '(1:10, :), ''', colors(mod(i, 6)), ''', ''linewidth'', 3);'));
end
hold off;