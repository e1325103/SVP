%% Simulate Flight Data
% Caclulate flight paths going from AT to European Countries using the
% travel statistics from 2011 and the coordiantes of the capitals

shr = 1000000;
wig = 3000000;

countries = readtable('countries.csv', 'Delimiter', ';');
travel = readtable('travelAT.csv', 'Delimiter', ';');
count = 1;
for v = travel.ID'
   if ~ismember(v, countries.ID)
       travel(count,:) = [];
       count = count - 1;
   end
   count = count + 1;
end

j = join(travel, countries, 'Keys', 'ID');

atpos(1) = countries(13, 3).LON;
atpos(2) = countries(13, 2).LAT;
connections = [];
for e = 1:size(j, 1)
    for c = 1:((j(e, 2).COUNT / 100) + 0)
        x = [(atpos(1) + rand(1) * wig) / shr, (j(e, 4).LON + rand(1) * wig) / shr];
        y = [(atpos(2) + rand(1) * wig) / shr, (j(e, 3).LAT + rand(1) * wig) / shr];
        connections = [connections; x, y];
    end
end