function [data_out] = cparFinalizeSampling(dev, data_in)
% [data_out] = cparFinalizeSampling(dev, data_in)

    cparStopSampling(dev);
    data_out.P01 = ConvertToArray(data_in.P01);
    data_out.P02 = ConvertToArray(data_in.P02);
end

function [x] = ConvertToArray(list)
    x = zeros(1, list.Count);
    
    for n = 0:list.Count - 1 
        x(n + 1) = list.Item(n);
    end
end