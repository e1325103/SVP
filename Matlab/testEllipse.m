x = rand(1000000, 1);
y = rand(1000000, 1);

h = repmat(0.5, size(x, 1), 1);
k = repmat(0.5, size(x, 1), 1);
A = pi*0.1;
a = 0.05;
b = 0.1;

i = ((((x - h)*cos(A) + (y - k)*sin(A)).^2) / (a * a)) + ((((x - h)*sin(A) + (y - k)*cos(A)) .^ 2) / (b+b));

scatter(x(i <= 1), y(i <= 1));