namespace Tera.ActionEngine
{
    /// <summary>
    /// Contains definitions for an action in respond to request.
    /// </summary>
    public interface IRequestAction
    {
        /// <summary>
        /// Player accepted request
        /// </summary>
        void Accepted();
        /// <summary>
        /// Player declined request
        /// </summary>
        void Declined();
    }
}
