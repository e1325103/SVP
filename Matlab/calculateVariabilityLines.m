A1 = repmat(1:10, 10,1);
A1 = A1 .*  repmat((0.1:0.1:1)', 1, 10) .* rand(10, 10);
A2 = (repmat(10, 10, 10) - A1) * 2;
B = repmat(1:10, 10,1);
A1 = [A1, B];
A2 = [A2, B];
A = [A1; A2];
streamlines = A';

%
subplot(3, 2, 1);
plot(streamlines(11:20, :), streamlines(1:10, :));
%
colors = 'rbgycm';
convInter = 0.95;
numBasis = 2;
numClusters = 4;

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

for i = 1:numClusters
    
end