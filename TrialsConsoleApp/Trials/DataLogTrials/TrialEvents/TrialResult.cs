using System.Linq;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class TrialResult
    {
        public TrialResult(
            DataLogTrial trial,
            DataLogTrialResponseEvent responseEvent,
            DataLogTrialPictureEvent firstTestPictureEvent,
            DataLogTrialPictureEvent endTestPictureEvent)
        {
            Trial = trial;
            ResponseEvent = responseEvent;
            FirstTestPictureEvent = firstTestPictureEvent;
            EndTestPictureEvent = endTestPictureEvent;
        }

        public DataLogTrial Trial { get; }
        public DataLogTrialResponseEvent ResponseEvent { get; }
        public DataLogTrialPictureEvent FirstTestPictureEvent { get; }
        public DataLogTrialPictureEvent EndTestPictureEvent { get; }

        public int ResponseTime => ResponseEvent.WorldTime - FirstTestPictureEvent.WorldTime;

        public bool UserResponseIsValid { get; private set; }
        public string Explanation { get; private set; }

        public TrialResult SetResponse(bool userResponseIsValid, string explanation)
        {
            UserResponseIsValid = userResponseIsValid;
            Explanation = explanation;
            return this;
        }

        public static TrialResult DetermineResult(DataLogTrial trial)
        {
            var result = new TrialResult(
                trial,
                (DataLogTrialResponseEvent)trial.Events.FirstOrDefault(e => e is DataLogTrialResponseEvent),
                (DataLogTrialPictureEvent)trial.Events.Where(e => e is DataLogTrialPictureEvent pictureEvent).Skip(1).First(),
                (DataLogTrialPictureEvent)trial.Events.First(e => e is DataLogTrialPictureEvent pe && pe.IsEndTrialPictureEvent));

            if (result.ResponseEvent == null)
            {
                // no response is given, thus picture must be nogo to be valid
                return result.FirstTestPictureEvent.IsNoGo
                    ? result.SetResponse(true, $"User correctly didn't respond to Nogo.")
                    : result.SetResponse(false, $"User incorrectly didn't respond to a picture with complex code.");
            }
            else
            {
                // too early?
                if (result.ResponseEvent.WorldTime < result.FirstTestPictureEvent.WorldTime)
                {
                    return result.SetResponse(false, $"User responded too early, before the testpicture was given.");
                }

                // too late?
                if (result.ResponseEvent.WorldTime >= result.EndTestPictureEvent.WorldTime)
                {
                    var should = result.FirstTestPictureEvent.IsNoGo
                        ? "also should not have responded at all to a NoGo"
                        : "should have responded";
                    return result.SetResponse(false, $"User responded too late and {should}.");
                }

                // correct response to the picture id?
                return result.FirstTestPictureEvent.IsNoGo
                    ? result.SetResponse(false, $"User incorrectly responded to a NoGo.")
                    : result.SetResponse(true, $"User correctly responded to a picture with a complex code.");
            }
        }
    }
}
