using Inputs.Data;

namespace Inputs
{
    public interface IInputConsumer
    {
        public bool CanConsumeInput(EInputType inputType);
    }
}

/* Integration example:
 *  Only define inputs that are to be consumed by the state the player is in.
 *  Inputs that aren't define will fall to the default and return false.
 * 
    public override bool CanConsumeInput(EInputType inputType)
    {
        return inputType switch
        {
            EInputType.Walk_Right => true,
            EInputType.Walk_Left => true,
            EInputType.Jump => JumpCount > 0,
            _ => false
        };
    }
 */