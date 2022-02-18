﻿using Cassandra.Data.Linq;
using Cassandra.Mapping;
using CassandraAPI.Interfaces;
using CassandraAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraAPI.app
{
    public class AirQuality
    {
        private ICassandraService Service { get; set; }

        public AirQuality(ICassandraService service)
        {
            Service = service;
        }
        public async Task<Guid>  SaveAirQualityTs(Airqualityts airqualityts)
        {
            IMapper mapper = new Mapper(Service.Session);
            airqualityts.stationid = Cassandra.TimeUuid.NewId();
            await mapper.InsertAsync(airqualityts);
            return airqualityts.stationid;
        }

        public async Task<List<Airqualityts>> GetAirqualitytsAsync(Guid uid)
        {
            var airqualityts = new Table<Airqualityts>(Service.Session);
            var airqualityts1 = await airqualityts.Where(s => s.stationid == uid).ExecuteAsync();
            return airqualityts1.OrderBy(s => s.measurementdate).ToList();
        }
    }
}
