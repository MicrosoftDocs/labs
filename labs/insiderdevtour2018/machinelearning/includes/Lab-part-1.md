## Intelligence at the edge

With the advent of cloud computing, and with more and more scenarios that involve taking advantage of Azure in order to scale better and reduce costs, a new term has started to appear and gain traction: intelligence at the edge. Intelligence at the edge, or edge computing, refers to processing the data as close to the source as possible, and allow systems to perform operational decisions directly, usually with the help of Machine Learning models. Instead of feeding the data back to the data center, processing it and then feeding back the actions to be performed, the whole process is performed close to the data (in the edge).

By processing data at the edge instead of forcing the data to do a complete round-trip to the servers, there are a number of immediate technical advantages: latency is reduced, quality of data available for processing is improved (since no discarding is required) and analysis results can be made available much faster. But the main draw of intelligence at the edge scenarios is not to do with the technical advantages themselves, but the scenarios they enable.

Having a car drive itself was something deemed part of a science fiction book just a few years ago. Detecting that a machine is about to fail in an industrial plant based on noise and vibration, and shutting down the assembly line to prevent further issues is now a common improvement in many industries. These two, among many other scenarios, are only made possible by the advent of intelligence at the edge: the ability to process the data as close to the source as possible.

Even though these new scenarios are tied to the new concept of the intelligent edge, the basis of the analysis and processing of the data to have a car drive itself or predict when machines are going to fail has its roots in a pretty stablished field: Artificial Intelligence and Machine Learning. Let's take a step back and get to know them a bit better.

## Intro to machine learning

### What is ML

Machine learning is a term coined in 1959 by Arthur Samuel, that currently defines a field of computer science. By leveraging statistical techniques, machine learning gives machines the ability to learn, improving their performance on a specific task. This is done by combining algorithms and huge amounts of data. By processing this previously collected data, ML algorithms build models that can predict the correct output when presented with a new input.

This learning capability is especially useful in scenarios where designing and programming explicit algorithms with good performance is difficult or infeasible. The most typical scenarios include detecting spam emails, optical character recognition, computer vision or recommendation algorithms.

The model-building phase is called "training." Once trained with existing data, the model can perform predictions with new, previously unseen data, which is called "inferencing," "evaluation," or "scoring."

### What is a model

The "machine learning model" is the output generated when a machine learning algorithm is trained with a training dataset. The process of training a machine learning model involves providing an algorithm with training data to learn from.

For example, considering a scenario where we want to detect spam email, the objective is to generate a model which would take in an input in real-time (an email), and generate an output (whether the email is considered spam or not). The training set would include normal and spam emails. Each email information (contents, header, sender domain, etc.) will be evaluated as per the choice of features (or attributes) and algorithm we made.

### What is ONNX

Open Neural Network Exchange (ONNX) provides an open source format for ML models, defining an extensible computation graph model, as well as definitions of built-in operators and standard data types. Initially its focused on the capabilities needed for inferencing (evaluation).

ONNX is being co-developed by Microsoft, Amazon and Facebook as an open-source project. In its initial release the project will support Caffe2, PyTorch, MXNet and Microsoft CNTK Deep learning framework.
