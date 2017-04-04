% EFME 3. assignment
% Nicolas Grossmann, 1325103

function [meanVector] = determineMeanVector(training)
% INPUT
% training ... training set

% OUTPUT
% meanVector ... mean vector
    meanVector = mean(training, 2);
end

