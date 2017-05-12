B = reducedA(:, id == 2);

X = mvnrnd(mean(B'),cov(B')*0.25,100);
Y = B';
d1 = mahal(X,Y);
figure;
hold on
% X = X(d1 < max(d1) * 0.1, :);
% d1 = d1(d1 < max(d1) * 0.1);
scatter(X(:,1),X(:,2),size(X, 1),d1,'+','LineWidth',1)
hb = colorbar;
scatter(Y(:,1),Y(:,2), 'ro', 'Linewidth', 2)
ylabel(hb,'Mahalanobis Distance')
legend('X','Y','Location','NW')
hold off

xlim([-40 40])
ylim([-15 15])