You are tasked with managing 3 different Kubernetes clusters running different web apps in Azure. Things have been going fine for a while now but today morning your users started complaining about performance and the issue seems to keep deteriorating. You have to meet your 5 minutes SLA to find and fix the issue. You are in PANIC mode – with so many containers how you can figure out what’s wrong so quickly. Don’t worry, we will lead you there - but it’s going to be up to you to find the root cause.

1. Login to the [Microsoft Azure portal](https://portal.azure.com)
2. Your Kubernetes cluster is part of the Azure Kubernetes Service (AKS). To get to AKS, search for "Kubernetes" in the top search bar and select "Kubernetes (preview)"
![searchkubernetes](images/kubernetes.png)
3. Your clusters are part of the Contoso IT – demo subscription.
4. **Task 1:** Click through the clusters, go through the CPU and memory usage and see if you can find any issue
5. So, you have isolated the problem to a specific container. Now you need to find out the root cause. To take a look at the logs, click on the Containers tab at the top and click on View Logs
6. Congratulations! You have found the root cause within the SLA and saved the day. While we are at it, we want to also check for security patches.
7. **Task 2** Find the process responsible for the performance issue

`#Use a query similar to this - INSERT QUERY`

8. Congratulations! You have found the root cause within the SLA and saved the day. While you are it, you have a bonus task.
9. Run a query to find all the VM’s that are missing security patches. 

`#Use a query similar to this - INSERT QUERY`


