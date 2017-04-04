function [recData] = reconstructData(U, numBasis, redData, meanVector)
    redU = U(:, 1:numBasis);
    recData = zeros(size(meanVector, 1), size(redData, 2));
    for c = 1:size(redData, 2)
        recData(:, c) = (redU * (redData(:, c))) + meanVector;
    end
end

