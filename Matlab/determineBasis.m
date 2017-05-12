% Nicolas Grossmann, 1325103

function [basis] = determineBasis(A, eigenvectors)
    basis = normc(A * eigenvectors);
end

