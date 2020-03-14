function cparPlot(data)
% cparPlot Plot data from the CPAR device.
%   cparPlot(data)

clf;
set(gcf, 'Color', [1 1 1]);

subplot(2,1,1);
plot(data.t, data.p01, 'r',...
     data.t, data.p02, 'b'); 
xlabel('Time [s]'); 
ylabel('Pressure [kPa]');
title('Pressure');
set(gca,'TickDir', 'out');
set(gca,'Box', 'off');
legend('1', '2');

subplot(2,1,2);
plot(data.t, data.vas, 'k');
xlabel('Time [s]'); 
ylabel('VAS [cm]');
title('Visual Analog Rating');
set(gca,'TickDir', 'out');
set(gca,'Box', 'off');
