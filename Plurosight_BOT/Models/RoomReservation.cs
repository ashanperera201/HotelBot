#region References
using Microsoft.Bot.Builder.FormFlow;
using Plurosight_BOT.Enums.common;
using System;
using System.Collections.Generic;
#endregion

#region Namespace
namespace Plurosight_BOT.Models
{
    [Serializable]
    public class RoomReservation
    {
        public SizeOptions? BedSize { get; set; }
        public int? NumberOfOccupents { get; set; }
        public DateTime? CheckInDate { get; set; }
        public int? NumerOfDaysToStay { get; set; }
        public List<AmenitiesOptions> Amenities { get; set; }
        public static IForm<RoomReservation> BuildForm()
        {
            return new FormBuilder<RoomReservation>()
                .Message("Welcome to the hotel reservation bot")
                .Build();
        }
    }
}
#endregion