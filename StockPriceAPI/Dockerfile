FROM python:3.9-slim-buster

# copy the requirements file into the image
COPY ./requirements.txt /app/requirements.txt

# switch working directory
WORKDIR /app

# install the dependencies and packages in the requirements file
RUN pip install --upgrade pip setuptools && \
    pip install -r requirements.txt

# copy every content from the local file to the image
COPY . /app

EXPOSE 5132

ENV FLASK_APP=app.py

CMD ["flask", "run", "--host", "0.0.0.0", "--port", "5132"]