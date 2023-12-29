import sys, datetime, csv

input = sys.stdin.read()

data = []

for line in input.splitlines():
    data.append(line)

data.pop(0)
data.pop()

dataset = []

dataset_aux = set()

for row in data:
    data_point = {
        'date': datetime.datetime.strptime(row[2:10], '%Y%m%d').day,
        'stock': row[12:24].strip(),
        'open': float(f'{row[56:67]}.{row[67:69]}'),
        'high': float(f'{row[69:80]}.{row[80:82]}'),
        'low': float(f'{row[82:93]}.{row[93:95]}'),
        'close': float(f'{row[108:119]}.{row[119:121]}')
    }

    key = (data_point['date'], data_point['stock'])
    if key not in dataset_aux:
        dataset_aux.add(key)
        dataset.append(data_point)

dataset = sorted(dataset, key=lambda x: (x['date'], x['stock']))

print(dataset[0])

csv_file = 'COTAHIST_2023.csv'
csv_headers = ['date', 'stock', 'open', 'high', 'low', 'close']

with open(csv_file, mode='w', newline='') as file:
    writer = csv.DictWriter(file, fieldnames=csv_headers)
    writer.writeheader()
    writer.writerows(dataset)

print(f'O arquivo CSV foi criado com sucesso: {csv_file}')