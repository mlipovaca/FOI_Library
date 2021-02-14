﻿using FOIKnjiznicaWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOIKnjiznicaWebServis.Controllers
{
    public class DodajPinController : ApiController
    {
        foiknjiznicaEntities db = new foiknjiznicaEntities();
        // GET: api/DodajPin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DodajPin/5
        public List<Object> Get(int id)
        {

            var upit = (from s in db.Clanovi_Auth_Protocol
                        where s.ClanoviId == id
                        select new
                        {
                            s.ClanoviId,
                            s.Auth_ProtocolId,
                            s.podaci
                        }) ;
            

            return upit.ToList<Object>();
        }

        // POST: api/DodajPin
        public void Post([FromBody]ClientAuth clijentAuth)
        {

            using (var ctx = new foiknjiznicaEntities())
            {
                var upit = db.Clanovi_Auth_Protocol.Where(x => x.ClanoviId == clijentAuth.ClanoviId).SingleOrDefault();
                if (upit != null)
                {
                    upit.ClanoviId = clijentAuth.ClanoviId;
                    upit.Auth_ProtocolId = clijentAuth.Auth_ProtocolId;
                    upit.podaci = clijentAuth.podaci;
                    //upit.odabrano = clijentAuth.odabrano;

                }
                else
                {
                    ctx.Clanovi_Auth_Protocol.Add(new Clanovi_Auth_Protocol
                    {
                        ClanoviId = clijentAuth.ClanoviId,
                        Auth_ProtocolId = clijentAuth.Auth_ProtocolId,
                        podaci = clijentAuth.podaci,
                        //odabrano = clijentAuth.odabrano
                    });
                }
                ctx.SaveChanges();
            }
        }

        // PUT: api/DodajPin/5
        public void Put( [FromBody]ClientAuth clijentAuth)
        {
            using (var ctx = new foiknjiznicaEntities())
            {
                var upit = db.Clanovi_Auth_Protocol.Where(x => x.ClanoviId == clijentAuth.ClanoviId).SingleOrDefault();
                if (upit != null)
                {
                    upit.ClanoviId = clijentAuth.ClanoviId;
                    upit.Auth_ProtocolId = clijentAuth.Auth_ProtocolId;
                    upit.podaci = clijentAuth.podaci;
                    //upit.odabrano = clijentAuth.odabrano;

                }
                else
                {
                    ctx.Clanovi_Auth_Protocol.Add(new Clanovi_Auth_Protocol()
                    {
                        ClanoviId = clijentAuth.ClanoviId,
                        Auth_ProtocolId = clijentAuth.Auth_ProtocolId,
                        podaci = clijentAuth.podaci,
                        //odabrano = clijentAuth.odabrano
                    });
                }
                ctx.SaveChanges();
            }

        }

        // DELETE: api/DodajPin/5
        public void Delete(int id)
        {
        }
    }
}
