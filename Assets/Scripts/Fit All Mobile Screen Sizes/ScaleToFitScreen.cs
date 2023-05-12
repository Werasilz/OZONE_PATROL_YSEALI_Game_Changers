using UnityEngine;

// Script from https://awesometuts.com/blog/support-mobile-screen-sizes-unity/?utm_medium=video&utm_source=youtube&utm_campaign=how_to_make_your_game_look_the_same_on_all_mobile_screen_sizes

namespace FitAllMobileScreenSizes
{
    [ExecuteInEditMode]
    public class ScaleToFitScreen : MonoBehaviour
    {
        private SpriteRenderer sr;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();

            // world height is always camera's orthographicSize * 2
            float worldScreenHeight = Camera.main.orthographicSize * 2;

            // world width is calculated by diving world height with screen heigh
            // then multiplying it with screen width
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            // to scale the game object we divide the world screen width with the
            // size x of the sprite, and we divide the world screen height with the
            // size y of the sprite
            transform.localScale = new Vector3(
                worldScreenWidth / sr.sprite.bounds.size.x,
                worldScreenHeight / sr.sprite.bounds.size.y, 1);
        }

    } // class
}
