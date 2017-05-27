%% Determine PCA Basis
function [basis] = determineBasis(A, eigenvectors)
    basis = normc(A * eigenvectors);
end

