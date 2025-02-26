import LocalShippingOutlinedIcon from '@mui/icons-material/LocalShippingOutlined';
import SupportAgentOutlinedIcon from '@mui/icons-material/SupportAgentOutlined';
import ProductionQuantityLimitsOutlinedIcon from '@mui/icons-material/ProductionQuantityLimitsOutlined';


const homeServiceItems = [
    {
        id: 0,
        serviceName: "Free Shipping",
        serviceDescription: "Enjoy free delivery on all orders over $500",
        icon: LocalShippingOutlinedIcon
    },
    {
        id: 1,
        serviceName: "24/7 Support",
        serviceDescription: "Our customer service team is always ready to help",
        icon: SupportAgentOutlinedIcon
    },
    {
        id: 2,
        serviceName: "30-Day Returns",
        serviceDescription: "Not satisfied? Return within 30 days for free",
        icon: ProductionQuantityLimitsOutlinedIcon
    },
];

export default homeServiceItems;