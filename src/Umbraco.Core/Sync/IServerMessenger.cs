﻿using System;
using System.Collections.Generic;
using umbraco.interfaces;

namespace Umbraco.Core.Sync
{
    /// <summary>
    /// Broadcasts distributed cache notifications to all servers of a load balanced environment.
    /// </summary>
    /// <remarks>Also ensures that the notification is processed on the local environment.</remarks>
    public interface IServerMessenger
    {
        // TODO
        // everything we do "by JSON" means that data is serialized then deserialized on the local server
        // we should stop using this, and instead use Notify() with an actual object that can be passed
        // around locally, and serialized for remote messaging - but that would break backward compat ;-(
        //
        // and then ServerMessengerBase must be able to handle Notify(), and all messengers too
        // and then ICacheRefresher (or INotifiableCacheRefresher?) must be able to handle it too
        //
        // >> v8

        /// <summary>
        /// Notifies the distributed cache, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="jsonPayload">The notification content.</param>
        void PerformRefresh(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, string jsonPayload);

        ///// <summary>
        ///// Notifies the distributed cache, for a specified <see cref="ICacheRefresher"/>.
        ///// </summary>
        ///// <param name="servers">The servers that compose the load balanced environment.</param>
        ///// <param name="refresher">The ICacheRefresher.</param>
        ///// <param name="payload">The notification content.</param>
        ///// <param name="serializer">A custom Json serializer.</param>
        //void Notify(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, object payload, Func<object, string> serializer = null);

        /// <summary>
        /// Notifies the distributed cache of specifieds item invalidation, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <typeparam name="T">The type of the invalidated items.</typeparam>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="getNumericId">A function returning the unique identifier of items.</param>
        /// <param name="instances">The invalidated items.</param>
        void PerformRefresh<T>(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, Func<T, int> getNumericId, params T[] instances);

        /// <summary>
        /// Notifies the distributed cache of specifieds item invalidation, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <typeparam name="T">The type of the invalidated items.</typeparam>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="getGuidId">A function returning the unique identifier of items.</param>
        /// <param name="instances">The invalidated items.</param>
        void PerformRefresh<T>(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, Func<T, Guid> getGuidId, params T[] instances);

        /// <summary>
        /// Notifies all servers of specified items removal, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <typeparam name="T">The type of the removed items.</typeparam>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="getNumericId">A function returning the unique identifier of items.</param>
        /// <param name="instances">The removed items.</param>
        void PerformRemove<T>(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, Func<T, int> getNumericId, params T[] instances);

        /// <summary>
        /// Notifies all servers of specified items removal, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="numericIds">The unique identifiers of the removed items.</param>
        void PerformRemove(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, params int[] numericIds);

        /// <summary>
        /// Notifies all servers of specified items invalidation, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="numericIds">The unique identifiers of the invalidated items.</param>
        void PerformRefresh(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, params int[] numericIds);

        /// <summary>
        /// Notifies all servers of specified items invalidation, for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        /// <param name="guidIds">The unique identifiers of the invalidated items.</param>
        void PerformRefresh(IEnumerable<IServerAddress> servers, ICacheRefresher refresher, params Guid[] guidIds);

        /// <summary>
        /// Notifies all servers of a global invalidation for a specified <see cref="ICacheRefresher"/>.
        /// </summary>
        /// <param name="servers">The servers that compose the load balanced environment.</param>
        /// <param name="refresher">The ICacheRefresher.</param>
        void PerformRefreshAll(IEnumerable<IServerAddress> servers, ICacheRefresher refresher);
    }
}