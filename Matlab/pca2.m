numBasis = 2;
A1 = repmat(1:10, 10,1);
A1 = A1 .* repmat((0.1:0.1:1)', 1, 10);
A2 = (repmat(10, 10, 10) - A1) * 2;
B = repmat(1:10, 10,1);
A1 = [A1, B];
A2 = [A2, B];
A = [A1; A2];
A = A';
subplot(3, 2, 1);
plot(A(11:20, :), A(1:10, :));

meanVector = determineMeanVector(A);

A = determineMeanSubtracted(A, meanVector);
subplot(3, 2, 2);
plot(A(11:20, :), A(1:10, :));
 
[eigenvectors, eigenvalues] = eig(A' * A, 'vector');
[eigenvectors, eigenvalues] = eigsort(eigenvectors, eigenvalues);
 
U = determineBasis(A, eigenvectors);
reducedA = reduceData(U, numBasis, A, meanVector);

id = kmeans(reducedA', 2);

C = [median(reducedA(:, id==1), 2)'; median(reducedA(:, id==2), 2)'];

subplot(3, 2, 3);
if (numBasis > 1)
    plot(reducedA(1, id==1),reducedA(2, id==1),'r.','MarkerSize',12);
    hold on;
    plot(reducedA(1, id==2),reducedA(2, id==2),'b.','MarkerSize',12);
    plot(C(:,1),C(:,2),'kx', 'MarkerSize',15,'LineWidth',3);
    hold off;
end
mvnpdf(reducedA)
reconstructedA = reconstructData(U, numBasis, reducedA, meanVector);
reconstructedC = reconstructData(U, numBasis, C', meanVector);
subplot(3, 2, 4);
plot(reconstructedA(11:20, (id == 1)), reconstructedA(1:10, (id == 1)), 'r');
hold on;
plot(reconstructedA(11:20, (id == 2)), reconstructedA(1:10, (id == 2)), 'b');
plot(reconstructedC(11:20, :), reconstructedC(1:10, :), 'black', 'linewidth', 2);
hold off;


subplot(3, 2, 5);
B = reducedA(:, id == 1);

X = mvnrnd(mean(B'),cov(B'),100);
Y = B';
d1 = mahal(X,Y);
d1 = (d1 - min(d1)) / (max(d1) - min(d1));
hold on

X = X(d1 < 0.1, :);
d1 = d1(d1 < 0.1);
scatter(X(:,1),X(:,2),size(X, 1),d1,'*','LineWidth',2)

B = reducedA(:, id == 2);

X = mvnrnd(mean(B'),cov(B'),10000);
Y = B';
d1 = mahal(X,Y);
d1 = (d1 - min(d1)) / (max(d1) - min(d1));
X = X(d1 < 0.1, :);
d1 = d1(d1 < 0.1);
scatter(X(:,1),X(:,2),size(X, 1),d1,'*','LineWidth',2);

hold off

reconstructedV = reconstructData(U, numBasis, X', meanVector);
plot(reconstructedV(11:20, :), reconstructedV(1:10, :), 'b', 'linewidth', 2);