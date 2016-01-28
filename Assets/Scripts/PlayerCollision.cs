using UnityEngine;

public class PlayerCollision : BaseCollision {

    protected override void HorizontalCollisions(ref Vector3 vel)
    {
        float directionX = Mathf.Sign(vel.x);
        float rayLength = Mathf.Abs(vel.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

			if (hit)
            {
                vel.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisionInfo.left = directionX == -1;
                collisionInfo.right = directionX == 1;

                Debug.Log("object hit" + hit.collider);
            }
			//Need to check to make sure transform != null because the script is constantly being ran
			if(hit.transform != null){
				//Check to see if the gameObject hit an enemy and the current gameObject is "Player"
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Enemy") && gameObject.tag.Equals("Player")) {
					//Enemy will cause damage to Player
					hit.transform.gameObject.GetComponent<Damage>().DoDamage(gameObject);
				}
			}

        }
    }

    protected override void VerticalCollisions(ref Vector3 vel)
    {
        float directionY = Mathf.Sign(vel.y);
        float rayLength = Mathf.Abs(vel.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                vel.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisionInfo.below = directionY == -1;
                collisionInfo.above = directionY == 1;

                Debug.Log("object hit" + hit.collider);
            }
			//Need to check to make sure transform != null because the script is constantly being ran
			if(hit.transform != null){
				//Check to see if the gameObject hit an enemy and the current gameObject is "Player"
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Enemy") && gameObject.tag.Equals("Player")) {
					//Enemy will cause damage to Player
					hit.transform.gameObject.GetComponent<Damage>().DoDamage(gameObject);
				}
			}
        }
    }

}
