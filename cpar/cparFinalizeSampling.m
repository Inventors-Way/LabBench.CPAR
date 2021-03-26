function [data] = cparFinalizeSampling(dev, data)
% [data_out] = cparFinalizeSampling(dev, data_in)

    cparStopSampling(dev);
    data.Pressure01 = ConvertToArray(data.Pressure01);
    data.Pressure02 = ConvertToArray(data.Pressure02);
    data.Target01 = ConvertToArray(data.Target01);
    data.Target02 = ConvertToArray(data.Target02);
    data.VAS = ConvertToArray(data.VAS);
    
    data.t = (0:length(data.Pressure01)-1)/20;
end

function [x] = ConvertToArray(list)
    x = zeros(1, list.Count);
    
    for n = 0:list.Count - 1 
        x(n + 1) = list.Item(n);
    end
end