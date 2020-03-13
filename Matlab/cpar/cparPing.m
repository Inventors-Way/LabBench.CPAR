function cparPing(dev)
% cparPing Ping a CPAR device.
%   cparPing(CPAR)
ping = Inventors.ECP.Functions.Ping;

try    
    dev.driver.Execute(ping);
    fprintf('Ping: %d\n', ping.Count);
catch
   fprintf('Ping failed\n'); 
end
