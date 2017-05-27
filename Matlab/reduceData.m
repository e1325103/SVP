%% Reduce Data with PCA
% Transform the Streamlines into the PCA space and reduce their dimensionality
function [redData] = reduceData(U, numBasis, data)
    redU = U(:, 1:numBasis);
    redData = zeros(numBasis, size(data, 2));
    for c = 1:size(data, 2)
        redData(:, c) = redU' * (data(:, c));
    end
end

