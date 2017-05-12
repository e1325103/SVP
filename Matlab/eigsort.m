% Nicolas Grossmann, 1325103

function [sortedVectors, sortedValues] = eigsort(eigenvectors, eigenvalues)
    eigenvectors = eigenvectors(:, eigenvalues > 10^-6);
    eigenvalues = eigenvalues(eigenvalues > 10^-6);

    [sortedValues, sortedIndices] = sort(eigenvalues, 'descend');
    sortedVectors = eigenvectors(:, sortedIndices);
end

