function [data] = cparGetData(dev)
    % cparGetData Get the data from a CPAR device.
    %    [data] = cparGetData(dev)

    ratings = dev.driver.Rating;
    data.vas = zeros(1, ratings.Count);
    data.t = zeros(1, ratings.Count);

    for n = 1:ratings.Count
       data.vas(n) = ratings.Item(n - 1) 
       data.t(n) = (n - 1)/20;
    end
    
    data.p01 = GetPressure(dev.driver.Channels.Item(0));
    data.p02 = GetPressure(dev.driver.Channels.Item(1));
end

function [p] = GetPressure(ch)
    values = ch.Pressure;
    p = zeros(1, values.Count);
    
    for n = 1:values.Count
       p(n) = values.Item(n - 1); 
    end
end