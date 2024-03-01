import { useEffect, useState } from "react";
import GetEventbusQueueResponse from "../interfaces/responses/eventbus-queue/get-eventbus-queue-response";
import { EventBusQueueService } from "../services/eventbus-queue-service";
import GetEventBusQueueListRequest from "../interfaces/requests/eventbus-queue/get-eventbus-queue-list-request";

const Home = () => {
    const [queues, setQueues] = useState<GetEventbusQueueResponse[]>([]);
    const queueService = new EventBusQueueService();
    const listRequest: GetEventBusQueueListRequest = {
        nameMatch: null,
        descriptionMatch: null,
        status: null,
        page: 1,
        pageSize: 1000,
        summarizeMessages: true
    }

    useEffect(() => {
        queueService.ListQueues(listRequest).then(response => {
            debugger;
            console.log(response);
            const apiResponse = response.data;

            if (apiResponse.data) {
                const queueList = apiResponse.data;
                console.log(queueList);
                setQueues(queueList);
                console.log(queues);
            }
        });
    }, []);

    return (
        <>
            <p>Home</p>
            {queues && queues.map(item => <p>{item.name} - {item.id}</p>)}
        </>
    );
}

export default Home;