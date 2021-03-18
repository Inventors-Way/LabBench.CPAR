function dev = cparGetDevice(id)
% cparGetDevice 

if LabBench.Instruments.InstrumentDB.Exists(id)
    record = LabBench.Instruments.InstrumentDB.Get(id);
    record.Used = 1;
    dev = record.Instrument;
    dev.PingEnabled = 1;
else
   fprintf("Instrument with ID = %s does not exists!\n", id);
   dev = 0;
end