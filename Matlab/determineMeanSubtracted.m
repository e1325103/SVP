% EFME 3. assignment
% Nicolas Grossmann, 1325103

function [trainingMeanSubtracted] = determineMeanSubtracted(training, meanVector)
% INPUT
% training ... training set
% meanVector ... mean vector of training set

% OUTPUT
% trainingMeanSubtracted ... training data minus mean vector (mean object)
    trainingMeanSubtracted = training - repmat(meanVector, 1, size(training, 2));
end

