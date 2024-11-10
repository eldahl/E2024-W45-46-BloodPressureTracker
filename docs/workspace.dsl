workspace {
    model {
        patient = person "Patient" "A patient who enters blood pressure measurements"
        doctor = person "Doctor" "Monitors the blood pressure measured by his patients"
        
        bps = softwareSystem "Blood Pressure System" "A system to collect blood pressure measurements by patients to be analysed by the patients doctor." {

            db = container "Database" "Stores patient and measurement data" {
                !docs Database
                tags "Database"
                technology "MySQL"
            }

            patientApp = container "Patient App" "App for patient to enter details about their latest blood pressure measurement" {
                !docs PatientApp
                tags "App"
                technology "iOS/Android"
            }
            patientService = container "Patient Service" "Responsible for handling data related to the patients" {
                !docs PatientService
                technology "REST"
                patientController = component "Patient Controller" "Responsible for getting requests related to fetching or storing new patient data"
                patientRepository = component "Patient Repository" "Responsible for storing and retrieving patient data"
            }
            
            patientApp -> patientController "GET"
            patientService -> db "Uses"
            
            doctorUI = container "Doctor UI" "Web interface for doctor to view and manage blood pressure measurements" {
                !docs DoctorUI
                tags "WebBrowser"
                technology "HTML/JS"
            }
            measurementService = container "Measurement Service" "Responsible for handling data related to the blood pressure measurements" {
                !docs MeasurementService
                technology "REST"
                measurementController = component "Measurement Controller" "Responsible for getting requests related to fetching or storing new and updated measurement data"
                measurementRepository = component "Measurement Repository" "Responsible for storing and retrieving measurement data"
            }
            
            doctorUI -> measurementController "GET/DELETE/PUT" {
                tags "REST"
            }
            doctorUI -> patientController "POST" {
                tags "REST"
            }
            measurementService -> db "Uses"
            patientApp -> measurementController "POST"

            patientController -> patientRepository "Uses"
            patientRepository -> db "Uses"

            measurementController -> measurementRepository "Uses"
            measurementRepository -> db "Uses"
        }
        
        patient -> patientApp "Enters data"
        doctor -> doctorUI "Works with data"
    }

    views {
        systemContext bps "SystemContext" {
            include *
            autolayout lr
        }
        
        container bps "BloodPressureSystem" {
            include *
            autolayout lr
        }

        component patientService "PatientService" {
            include *
            autolayout lr
        }

        component measurementService "MeasurementService" {
            include *
            autolayout lr
        }
        
        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #9b191f
                shape person
            }
            element "Software System" {
                background #ba1e25
            }
            element "App" {
                shape "MobileDeviceLandscape"
            }
            element "Database" {
                shape cylinder
            }
            element "Container" {
                background #d9232b
            }
            element "Component" {
                background #E66C5A
            }
            element "WebBrowser" {
                shape WebBrowser
            }
        }
    }
}