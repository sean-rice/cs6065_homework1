# sudo for basically everything
sudo yum install -y git

# installing docker
sudo amazon-linux-extras install docker
sudo service docker start
sudo chkconfig docker on
sudo usermod -a -G docker ec2_user

# installing docker-compose
sudo curl -L "https://github.com/docker/compose/releases/download/1.27.4/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

# get code
git clone https://github.com/sean-rice/cs6065_homework1.git
cd ./cs6065_homework1/docker
docker-compose build

# to run:
# docker-compose up -d
