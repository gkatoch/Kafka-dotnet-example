- To run this project for publishing and subscribing Kafka messages you need Kafka and Zokeeper running.
- Kafka and Zookeeper binaries can be downloaded from the URL below:
http://zookeeper.apache.org/releases.html
http://kafka.apache.org/downloads.html

Please follow the below steps for configuring and running Zookeeper and Kafka:
a. Change dataDir=\zookeeper-3.4.10\data in zoo.cfg file
b. Change log.dirs=C:\kafka_2.11-1.1.0\kafka_2.11-1.1.0\kafka-logs or any other location on your machine in server.properties file of Kafka. 
c. Check if server.properties file contain localhost mapping to zookeper on same port as of zoo.cfg file.
d. Run the following commands:
	1. Run Zookeeper by running command zkserver at path:  .\zookeeper-3.4.10.tar\zookeeper-3.4.10\bin. You could also configure this in system paths
	2. Run Kafka by executing command on Kafka home directory: .\bin\windows\kafka-server-start.bat .\config\server.properties

e. Zookeeper and Kafka should be running at this point of time

- This example uses both kafka-net and Confluent.Kafka nugget packages to illustrate the usage. Code for Kafka-net is commented out in projects.


- Some useful Kafka Commands:

1. List Topics: kafka-topics.bat --list --zookeeper localhost:2181
2. Describe Topic: kafka-topics.bat --describe --zookeeper localhost:2181 --topic [Topic Name]
3. Read messages from beginning: kafka-console-consumer.bat --zookeeper localhost:2181 --topic [Topic Name] --from-beginning
4. Delete Topic: kafka-run-class.bat kafka.admin.TopicCommand --delete --topic [topic_to_delete] --zookeeper localhost:2181
5. Create Topic: kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic [Topic Name]
6. Kafka Consumer: kafka-console-producer.bat --broker-list localhost:9092 --topic [Topic Name]
7. Kafka Producer: kafka-console-consumer.bat --bootstrap-server localhost:2181 --topic [Topic Name]
