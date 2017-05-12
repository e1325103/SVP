% Nicolas Grossmann, 1325103

function [error] = reconstructEval(U, dataset, meanVector)
    error = zeros(1, size(U, 2));
    for l = 1:size(U, 2)
        redU = U(:, 1:l);
        recDataset = zeros(size(dataset));
        for c = 1:size(dataset, 2)
            recData = redU * (redU' * (dataset(:, c) - meanVector));
            recDataset(:, c) = recData + meanVector;
        end
        error(l) = mean(sqrt(sum((recDataset - dataset) .^ 2)));
    end
end

