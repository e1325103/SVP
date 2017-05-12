% Nicolas Grossmann, 1325103

function [trainingMeanSubtracted] = determineMeanSubtracted(training, meanVector)
    trainingMeanSubtracted = training - repmat(meanVector, 1, size(training, 2));
end

