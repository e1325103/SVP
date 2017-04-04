% EFME 3. assignment
% Nicolas Grossmann, 1325103

function [sortedVectors, sortedValues] = eigsort(eigenvectors, eigenvalues)
% INPUT
% eigenvectors
% eigenvalues

% OUTPUT
% sortedVectors ... sorted Eigenvectors (decending)
% sortedValues ... sorted Eigenvalues (decending)

 % remove eigenfaces with eigenvalues close or equal to zero
    eigenvectors = eigenvectors(:, eigenvalues > 10^-6);
    eigenvalues = eigenvalues(eigenvalues > 10^-6);

    [sortedValues, sortedIndices] = sort(eigenvalues, 'descend');
    sortedVectors = eigenvectors(:, sortedIndices);
end

