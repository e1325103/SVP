% EFME 3. assignment
% Nicolas Grossmann, 1325103

function [basis] = determineBasis(A, eigenvectors)
% INPUT
% A ... training data minus mean vector
% eigenvectors

% OUTPUT
% basis ... matrix of normalized basis vectors
    basis = normc(A * eigenvectors);
end

