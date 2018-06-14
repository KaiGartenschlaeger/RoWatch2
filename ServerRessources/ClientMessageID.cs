namespace ServerRessources
{
    public enum ClientMessageId
    {
        /// <summary>
        /// Client hat sich neu verbunden und sendet das erste mal die Daten.
        /// </summary>
        Connected,

        /// <summary>
        /// Ein Wert hat sich verändert.
        /// </summary>
        PropertieChanged,

        /// <summary>
        /// Client hat sich abgemeldet.
        /// </summary>
        Disconnected
    }
}