#region References
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Plurosight_BOT.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
#endregion

#region Namespace
namespace Plurosight_BOT.Dialogs
{
    [LuisModel("b7afffce-609c-4e04-995d-7ce1ef5b0063", "680feb6b295f4ed3857929883568093d")]
    [Serializable]
    public class LUISDialog : LuisDialog<RoomReservation>
    {
        /// <summary>
        /// The reserve room
        /// </summary>
        private readonly BuildFormDelegate<RoomReservation> _reserveRoom;
        /// <summary>
        /// Initializes a new instance of the <see cref="LUISDialog" /> class.
        /// </summary>
        /// <param name="reserveRoom">The reserve room.</param>
        public LUISDialog(BuildFormDelegate<RoomReservation> reserveRoom)
        {
            _reserveRoom = reserveRoom;
        }
        /// <summary>
        /// Nones the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry i don't know what you mean.");
            context.Wait(MessageReceived);
        }
        /// <summary>
        /// Greetings the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [LuisIntent("Greetings")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreeetingDialog(), Callback);
        }
        /// <summary>
        /// Callbacks the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
        /// <summary>
        /// Rooms the reservation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [LuisIntent("Reservation")]
        public async Task RoomReservation(IDialogContext context, LuisResult result)
        {
            var enrollmentForm = new FormDialog<RoomReservation>(new RoomReservation(), _reserveRoom, FormOptions.PromptInStart);
            context.Call<RoomReservation>(enrollmentForm, Callback);
        }
        /// <summary>
        /// Queries the amenities.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [LuisIntent("QueryAmenities")]
        public async Task QueryAmenities(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities.Where(entity => entity.Type == "Amenity"))
            {
                var value = entity.Entity.ToLower();
                if (value == "pool" || value == "gym" || value == "wifi" || value == "towels")
                {
                    await context.PostAsync("Yes we have that!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("I'm sorry we don't have that.");
                    context.Wait(MessageReceived);
                    return;
                }
            }
            await context.PostAsync("I'm sorry we don't have that.");
            context.Wait(MessageReceived);
            return;
        }
    }
}
#endregion