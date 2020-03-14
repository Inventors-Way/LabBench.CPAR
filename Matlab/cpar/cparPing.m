function cparPing(dev, enable)
% cparPing Ping a CPAR device.
%   cparPing(dev, enable)

if enable
    ping = Inventors.ECP.Functions.Ping;

    try    
        dev.Execute(ping);
        dev.Ping = true;
        fprintf('Ping: %d\n', ping.Count);
    catch
       fprintf('Ping failed\n'); 
    end
else
   dev.Ping = false; 
end