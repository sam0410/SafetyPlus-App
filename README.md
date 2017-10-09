-----------------------------------------------------------
SAFETY APP 
By- Team YOLO (Code.Fun.Do IIT BHU) January 2017

Team members-
1.	Kavita Sidar
2.	Shweta Singh
3.	Samikshya Chand
-----------------------------------------------------------

This is an app for Windows Phone 8.1 which aims at the safety of people.
A citizen can alert policemen in an emergency. The police on the other hand can locate the people in need on their side of the app.
Both the police and the citizens need to register themselves before using the app for the first time.
 
-----------------------------------------------------------
Registration on the App
-----------------------------------------------------------

The citizens have to provide their Name,Aadhaar Card (UID) number, email-ID, Mobile number and address for registering on the app.
The police have to provide a unique authenticated key provided to them for registration on the app.They also have to add their mobile number and their police station.
All these details are stored in Windows Azure cloud.
Registration page of the user opens when he opens the app for the first time.In subsequent uses, there is no need to enter any details.
(This is because we store the details in a local database of the mobile).

-----------------------------------------------------------           
  Basic Working 
-----------------------------------------------------------

 The citizens in crisis would press an emergency button which in turn will mark their current location on the map shown to the police (along with the Aadhar card number of the citizen)

This helps the police to know about the emergencies as soon as they occur and take action without any delay.

Also, after the emergency is solved,the police can enter the Aadhar number of the citizen,this would delete the emergency alert of the citizen.

 

-----------------------------------------------------------            
 Managing the location in the emulator
-----------------------------------------------------------

The coordinate returned in 
await geoLocator.GetGeopositionAsync();
always points to Microsoft HQ in Redmond.

However,for checking for various locations the following steps were followed.
 
1.   Set a current location in the emulator.

2.   Run my app. It reports the current location as  Redmond.

3.   Run the Maps application. It correctly goes to the  location set in step 1.

4.   Run my app again. Now it uses the desired location.


-----------------------------------------------------------             
Additional features that can be added in future
-----------------------------------------------------------

1.	Real time tracking of the person in an emergency can be added

2.	A feature in which a particular policeman attending to an emergency can mark the user's location on the map in a different color so that multiple policemen don't attend to the same emergency.

3.	Send notifications of emergencies to policemen in a radius of 10 km. This will ensure that even if the police app is off, they would be notified about the emergencies.

4.	Geofencing feature can also be utilised in the app. In 	which all the people having the app in a radius of 1 km would get 	notification of the emergency.

5.	Automatic video and audio recording feature can be added so that the citizen might show evidences of wrong goings around him to the police.

6.	We may add different kind of emergencies such as hospital emergency, fire emergencies which in turn sends the user's 	location to hospital ambulance services and fire station respectively.


----------------------------------------------------------------
