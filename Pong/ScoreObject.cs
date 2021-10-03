namespace Pong {
    public class ScoreObject {

        private float screenSpaceX;
        

        /// <summary>
        /// Gets the percentage of width of the screen that will count as a score (left and right)
        /// </summary>
        public float GetSSX() {
            return screenSpaceX;
        }


        /// <summary>
        /// Sets the percentage of width of the screen that will count as a score (left and right)
        /// </summary>
        public void SetSSX(float percScreenSpaceXParam) {
            screenSpaceX = percScreenSpaceXParam;
        }


        public ScoreObject(int percScreenSpaceXParam, int screenWidth) {

            float screenSpaceX = screenWidth * (percScreenSpaceXParam / 100);
            SetSSX(screenSpaceX);
        }
    }
}