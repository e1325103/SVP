close all;
shr = 1000000;
wig = 3000000;
load coastlines

countries = readtable('countries.csv', 'Delimiter', ';');
travel = readtable('travelAT.csv', 'Delimiter', ';');
scatter(table2array(countries(:, 3)) / shr, table2array(countries(:, 2)) / shr);
hold on;
plot(coastlon, coastlat);
hold off;
count = 1;
for v = travel.ID'
   if ~ismember(v, countries.ID)
       travel(count,:) = [];
       count = count - 1;
   end
   count = count + 1;
end
j = join(travel, countries, 'Keys', 'ID');

figure;
hold on;
plot(coastlon, coastlat);
atpos(1) = countries(13, 3).LON;
atpos(2) = countries(13, 2).LAT;
connections = [];
for e = 1:size(j, 1)
    for c = 1:((j(e, 2).COUNT / 100) + 0)
        x = [(atpos(1) + rand(1) * wig) / shr, (j(e, 4).LON + rand(1) * wig) / shr];
        y = [(atpos(2) + rand(1) * wig) / shr, (j(e, 3).LAT + rand(1) * wig) / shr];
        connections = [connections; x, y];
        plot(x, y);
    end
    j(e, 5).NAME
end
hold off;
xlim([-10 40])
ylim([-20 80])