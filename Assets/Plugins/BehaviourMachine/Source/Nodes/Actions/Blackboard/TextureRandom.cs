//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Randomly selects a value in a set of textures.
    /// </summary>
    [NodeInfo ( category = "Action/Blackboard/",
                icon = "Blackboard",
                description = "Randomly selects a texture in a set of textures")]
    public class TextureRandom : ActionNode {

        /// <summary>
        /// The possible texture values.
        /// </summary>
        [VariableInfo(tooltip = "The possible texture values")]
        public TextureVar[] textures;

        /// <summary>
        /// Store the random selected texture.
        /// </summary>
        [VariableInfo(canBeConstant = false, tooltip = "Store the random selected texture")]
        public TextureVar storeTexture;

        public override void Reset () {
            textures = new TextureVar[] {new ConcreteTextureVar(null), new ConcreteTextureVar(null)};
            storeTexture = new ConcreteTextureVar();
        }

        public override Status Update () {
            // Validate members
            if (textures.Length == 0 || storeTexture.isNone)
                return Status.Error;

            // Randomly selects a texture
            storeTexture.Value = textures[Random.Range(0, textures.Length)].Value;
            return Status.Success;
        }

    }
}