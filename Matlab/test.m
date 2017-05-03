B = reducedA(:, id == 2);

X = mvnrnd(mean(B'),cov(B'),100);
Y = B';
d1 = mahal(X,Y);
scatter(Y(:,1),Y(:,2))
hold on
X = X(d1 < max(d1) * 0.1, :);
d1 = d1(d1 < max(d1) * 0.1);
scatter(X(:,1),X(:,2),size(X, 1),d1,'*','LineWidth',2)
hb = colorbar;
ylabel(hb,'Mahalanobis Distance')
legend('X','Y','Location','NW')
hold off

xlim([-40 40])
ylim([-15 15])