//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Calls a method named "Method Name" in the "Game Object".
    /// </summary>
    [NodeInfo ( category = "Action/GameObject/",
                icon = "GameObject",
                description = "Calls a method named \"Method Name\" in every MonoBehaviour in the \"Game Object\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/SendMessageOptions.html")]
    public class SendMessage : ActionNode {

        public enum MessageType {
            SendMessage,
            SendMessageUpwards,
            BroadcastMessage
        }

        /// <summary>
        /// The game object to SendMessage.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The game object to SendMessage")]
        public GameObjectVar gameObject;

        /// <summary>
        /// Name of the method to call.
        /// </summary>
        [VariableInfo(tooltip = "Name of the method to call")]
        public StringVar methodName;

        /// <summary>
        /// SendMessage, call methods only on the "Game Object". SendMessageUpwards, call methods on the "Game Object" and on its ancestors. BroadcastMessage, call methods on the "Game Object" and on its children.
        /// </summary>
        [Tooltip("SendMessage, call methods only on the \"Game Object\". SendMessageUpwards, call methods on the \"Game Object\" and on its ancestors. BroadcastMessage, call methods on the \"Game Object\" and on its children")]
        public MessageType messageType;

        /// <summary>
        /// Options for how to send a message.
        /// </summary>
        [Tooltip("Options for how to send a message")]
        public SendMessageOptions options = SendMessageOptions.RequireReceiver;

        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The float parameter")]
        public FloatVar floatParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The int parameter")]
        public IntVar intParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The bool parameter")]
        public BoolVar boolParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The string parameter")]
        public StringVar stringParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Vector3 parameter")]
        public Vector3Var vector3Parameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Rect parameter")]
        public RectVar rectParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Color parameter")]
        public ColorVar colorParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Quaternion parameter")]
        public QuaternionVar quaternionParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Game Object parameter")]
        public GameObjectVar gameObjectParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Texture parameter")]
        public  TextureVar textureParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Material parameter")]
        public MaterialVar materialParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The Object parameter")]
        public ObjectVar objectParameter;
        [VariableInfo(requiredField = false, nullLabel = "Don't Use", tooltip = "The FsmEvent parameter")]
        public FsmEvent fsmEventParameter;

        public override void Reset () {
            gameObject = new ConcreteGameObjectVar(this.self);
            methodName = new ConcreteStringVar();
            options = SendMessageOptions.RequireReceiver;
            floatParameter = new ConcreteFloatVar();
            intParameter = new ConcreteIntVar();
            boolParameter = new ConcreteBoolVar();
            stringParameter = new ConcreteStringVar();
            vector3Parameter = new ConcreteVector3Var();
            rectParameter = new ConcreteRectVar();
            colorParameter = new ConcreteColorVar();
            quaternionParameter = new ConcreteQuaternionVar();
            gameObjectParameter = new ConcreteGameObjectVar(this.self);
            textureParameter = new ConcreteTextureVar();
            materialParameter = new ConcreteMaterialVar();
            fsmEventParameter = new ConcreteFsmEvent();
            objectParameter = new ConcreteObjectVar();
            messageType = MessageType.SendMessage;
        }

        public override Status Update () {
            // Validate members
            if (gameObject.Value == null)
                return Status.Error;

            // Get parameter
            object parameter = null;
            if (!floatParameter.isNone)
                parameter = floatParameter.Value;
            else if (!intParameter.isNone)
                parameter = intParameter.Value;
            else if (!intParameter.isNone)
                parameter = intParameter.Value;
            else if (!boolParameter.isNone)
                parameter = boolParameter.Value;
            else if (!stringParameter.isNone)
                parameter = stringParameter.Value;
            else if (!vector3Parameter.isNone)
                parameter = vector3Parameter.Value;
            else if (!rectParameter.isNone)
                parameter = rectParameter.Value;
            else if (!colorParameter.isNone)
                parameter = colorParameter.Value;
            else if (!quaternionParameter.isNone)
                parameter = quaternionParameter.Value;
            else if (!gameObjectParameter.isNone)
                parameter = gameObjectParameter.Value;
             else if (!textureParameter.isNone)
                parameter = textureParameter.Value;
            else if (!materialParameter.isNone)
                parameter = materialParameter.Value;
            else if (!objectParameter.isNone)
                parameter = objectParameter.Value;
            else if (!fsmEventParameter.isNone)
                parameter = fsmEventParameter.id;

            // Send message
            switch (messageType) {
                case MessageType.SendMessage:
                    gameObject.Value.SendMessage(methodName.Value, parameter, options);
                    break;
                case MessageType.SendMessageUpwards:
                    gameObject.Value.SendMessageUpwards(methodName.Value, parameter, options);
                    break;
                case MessageType.BroadcastMessage:
                    gameObject.Value.BroadcastMessage(methodName.Value, parameter, options);
                    break;
            }
            
            return Status.Success;
        }
    }
}