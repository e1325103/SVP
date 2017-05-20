d = [dlmread('D:\facebook\0.edges'); dlmread('D:\facebook\107.edges');dlmread('D:\facebook\348.edges'); dlmread('D:\facebook\414.edges'); dlmread('D:\facebook\686.edges'); dlmread('D:\facebook\1684.edges'); dlmread('D:\facebook\1912.edges'); dlmread('D:\facebook\3437.edges'); dlmread('D:\facebook\3980.edges')];
d = d + 1;
adj=sparse(d(:,1),d(:,2),1);
adj( ~any(adj,2), : ) = [];  %rows
adj( :, ~any(adj,1) ) = [];  %columns
g = graph(adj);
comps = conncomp(g);
comps = (comps == median(comps));
adj = adj(comps, comps);
g = graph(adj);
p = plot(g,'Layout','force');
pos = [p.XData; p.YData]';
[e1, e2] = find(adj > 0);
e = [e1, e2];
A = zeros(20, size(e, 1));
for i = 1:size(e, 1)
    p1 = pos(e(i, 1), :);
    p2 = pos(e(i, 2), :);
    A(1:10, i) = linspace(p1(:, 1), p2(:, 1), 10);
    A(11:20, i) = linspace(p1(:, 2), p2(:, 2), 10);
end