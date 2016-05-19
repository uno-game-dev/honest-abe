//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Gets the device location (longitude, latitude and altitude).
    /// <summary>
    [NodeInfo(  category = "Action/Input/",
    			icon = "BuildSettings.iPhone.Small",
                description = "",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Input-location.html")]
	public class GetDeviceLocation : ActionNode {

		/// <summary>
		/// Desired service accuracy in meters.
		/// </summary>
		[VariableInfo(tooltip = "Desired service accuracy in meters")]
		public FloatVar desiredAccuracy;

		/// <summary>
		/// The minimum distance (measured in meters) a device must move laterally before Input.
		/// </summary>
		[VariableInfo(tooltip = "The minimum distance (measured in meters) a device must move laterally before Input")]
		public FloatVar updateDistance;

		/// <summary>
		/// The device location. Vector3 (x = longitude, y = latitude, z = altitude).
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "The device location. Vector3 (x = longitude, y = latitude, z = altitude)")]
		public Vector3Var storeLocation;

		/// <summary>
		/// Store the device longitude.
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the device longitude")]
		public FloatVar storeLongitude;

		/// <summary>
		/// Store the device latitude.
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the device latitude")]
		public FloatVar storeLatitude;

		/// <summary>
		/// Store the device altitude.
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the device altitude")]
		public FloatVar storeAltitude;

		/// <summary>
		/// Store the horizontal accuracy of the location.
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the horizontal accuracy of the location")]
		public FloatVar storeHorizontalAccuracy;

		/// <summary>
		/// Store the vertical accuracy of the location.
		/// </summary>
		[VariableInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Store", tooltip = "Store the vertical accuracy of the location")]
		public FloatVar storeVerticalAccuracy;

		public override void Reset () {
			desiredAccuracy = 10f;
			updateDistance = 10f;
			storeLocation = new ConcreteVector3Var();
			storeLongitude = new ConcreteFloatVar();
			storeLatitude = new ConcreteFloatVar();
			storeAltitude = new ConcreteFloatVar();
			storeHorizontalAccuracy = new ConcreteFloatVar();
			storeVerticalAccuracy = new ConcreteFloatVar();
		}

		/// <summary>
		/// Starts location service updates.
		/// </summary>
		public override void Start () {
			if (Input.location.isEnabledByUser)
				Input.location.Start(desiredAccuracy.Value, updateDistance.Value);
		}

		public override Status Update () {
			// The location service is not enabled in user settings?
			if (!Input.location.isEnabledByUser)
				return Status.Failure;

			// The location service is initializing?
			if (Input.location.status == LocationServiceStatus.Initializing)
				return Status.Running;

			// The location service failed?
			if (Input.location.status == LocationServiceStatus.Failed)
				return Status.Failure;

			// Get the location info
			LocationInfo locationInfo = Input.location.lastData;
			storeLocation.Value = new Vector3(locationInfo.longitude, locationInfo.latitude, locationInfo.altitude);
			storeLongitude.Value = locationInfo.longitude;
			storeLatitude.Value = locationInfo.latitude;
			storeAltitude.Value = locationInfo.altitude;
			storeHorizontalAccuracy = locationInfo.horizontalAccuracy;
			storeVerticalAccuracy = locationInfo.verticalAccuracy;

			return Status.Success;
		}

		/// <summary>
		/// Stops location service updates.
		/// </summary>
		public override void End () {
			if (Input.location.isEnabledByUser)
				Input.location.Stop();
		}
	}
}
