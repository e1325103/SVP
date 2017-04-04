function [redData] = reduceData(U, numBasis, data, meanVector)
    redU = U(:, 1:numBasis);
    redData = zeros(numBasis, size(data, 2));
    for c = 1:size(data, 2)
        redData(:, c) = redU' * (data(:, c));
    end
end

