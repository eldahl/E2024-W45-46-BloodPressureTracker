import os
import time
import tinytuya
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from datetime import datetime

pp_dev_id = os.environ["PP_DEV_ID"]
pp_ip_addr = os.environ["PP_IP"]
pp_local_key = os.environ["PP_LOCAL_KEY"]

# Initialize plot
fig, ax = plt.subplots()
time_values = []
electricity_values = []
current_values = []
power_values = []
voltage_values = []

# Function to update plot
def update(frame):
    data = extract_and_replace_keys(d.status())
    print(data)
    current_time = datetime.now().strftime('%H:%M:%S')

    # Append new data
    time_values.append(current_time)
    electricity_values.append(data['Add Electricity (kwh)'])
    current_values.append(data['Current (mA)'])
    power_values.append(data['Power (W)'])
    voltage_values.append(data['Voltage (V)'])

    # Clear and plot again
    ax.clear()
    ax.plot(time_values, electricity_values, label='Add Electricity (kwh)')
    ax.plot(time_values, current_values, label='Current (mA)')
    ax.plot(time_values, power_values, label='Power (W)')
    ax.plot(time_values, voltage_values, label='Voltage (V)')

    plt.xticks(rotation=45, ha='right')
    plt.tight_layout()
    plt.legend(loc='upper left')
    plt.xlabel('Time')
    plt.ylabel('Values')
    plt.title('Real-Time Plot of Electrical Measurements')

def extract_and_replace_keys(data):
    key_mapping = {
        '17': 'Add Electricity (kwh)',
        '18': 'Current (mA)',
        '19': 'Power (W)',
        '20': 'Voltage (V)'
    }
    extracted_values = {}
    for key, label in key_mapping.items():
        value = data['dps'].get(key)
        # Apply voltage conversion if the key is '20'
        if key == '20' or key == '19' and value is not None:
            value = round(value / 10, 1)
        extracted_values[label] = value
    return extracted_values

d = tinytuya.OutletDevice(dev_id=pp_dev_id,
                          address=pp_ip_addr,      # Or set to 'Auto' to auto-discover IP address
                          local_key=pp_local_key, 
                          version=3.3)
# Run the animation
ani = animation.FuncAnimation(fig, update, interval=10000)

plt.show()
