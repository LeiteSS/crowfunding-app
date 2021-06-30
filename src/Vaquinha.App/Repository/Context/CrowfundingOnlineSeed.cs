using System;
using System.Collections.Generic;
using System.Linq;
using Vaquinha.App.Entities;

namespace Vaquinha.App.Repository.Context
{
    public class CrowfundingOnlineSeed
    {
        public static void Seed(CrowfundingOnlineDBContext context)
        {
            if (!context.Causes.Any())
            {
                var causas = new List<Cause> {
                    new Cause(Guid.NewGuid(), "Santa Casa", "Araraquara", "SP"),
                    new Cause(Guid.NewGuid(), "Amigos do bem", "Araraquara", "SP"),
                    new Cause(Guid.NewGuid(), "A Passos", "São Carlos", "SP")
                };

                context.AddRange(causas);
                context.SaveChanges();
            }
        }
    }
}