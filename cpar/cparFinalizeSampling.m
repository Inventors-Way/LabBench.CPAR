function [data_out] = cparFinalizeSampling(dev, data_in)
% [data_out] = cparFinalizeSampling(dev, data_in)

    cparStopSampling(dev);
    data_out.Pressure01 = ConvertToArray(data_in.Pressure01);
    data_out.Pressure02 = ConvertToArray(data_in.Pressure02);
    data_out.Target01 = ConvertToArray(data_in.Target01);
    data_out.Target02 = ConvertToArray(data_in.Target02);
    data_out.VAS = ConvertToArray(data_in.VAS);
    data_out.Final01 = data_in.Final01;
    data_out.Final02 = data_in.Final02;
    data_out.FinalVAS = data_in.FinalVAS;
end

function [x] = ConvertToArray(list)
    x = zeros(1, list.Count);
    
    for n = 0:list.Count - 1 
        x(n + 1) = list.Item(n);
    end
end