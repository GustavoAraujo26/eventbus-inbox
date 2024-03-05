import { Route, Routes } from "react-router-dom";
import BasicPage from "./pages/basic-page";
import Home from "./pages/home";
import EventBusQueuesDashboard from "./pages/event-bus-queues/eventbus-queues-dashboard";
import EventBusQueueForm from "./pages/event-bus-queues/eventbus-queue-form";
import EventBusMessagesDashboard from "./pages/event-bus-messages/eventbus-messages-dashboard";
import EventBusMessageForm from "./pages/event-bus-messages/eventbus-message-form";
import EventBusMessageDetails from "./pages/event-bus-messages/eventbus-message-details";
import EventBusQueueDetails from "./pages/event-bus-queues/eventbus-queue-details";
import EventBusMessageSender from "./pages/event-bus-message-sender";

function App() {
  return (
    <Routes>
      <Route element={<BasicPage/>}>
        <Route path="/" element={<Home/>} />
        
        <Route path="eventbus-queues/dashboard" element={<EventBusQueuesDashboard/>} />
        <Route path="eventbus-queues/new" element={<EventBusQueueForm/>} />
        <Route path="eventbus-queues/:id" element={<EventBusQueueForm/>} />
        <Route path="eventbus-queues/details/:id" element={<EventBusQueueDetails/>} />

        <Route path="eventbus-messages/dashboard" element={<EventBusMessagesDashboard/>} />
        <Route path="eventbus-messages/new" element={<EventBusMessageForm/>} />
        <Route path="eventbus-messages/:id" element={<EventBusMessageForm/>} />
        <Route path="eventbus-messages/send" element={<EventBusMessageSender/>} />
        <Route path="eventbus-messages/details/:id" element={<EventBusMessageDetails/>} />
      </Route>
    </Routes>
  );
}

export default App;
