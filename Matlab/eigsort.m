%% Sort Eigenvectors
% Sort the eigenvectors based on their eigenvalues in decending order
function [sortedVectors, sortedValues] = eigsort(eigenvectors, eigenvalues)
    eigenvectors = eigenvectors(:, eigenvalues > 10^-6);
    eigenvalues = eigenvalues(eigenvalues > 10^-6);

    [sortedValues, sortedIndices] = sort(eigenvalues, 'descend');
    sortedVectors = eigenvectors(:, sortedIndices);
end